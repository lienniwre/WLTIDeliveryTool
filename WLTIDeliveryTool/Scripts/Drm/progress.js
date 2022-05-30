$(document).ready(function () {

    $('#tblProgress').DataTable({
        "ajax": {
            "url": "/drm/displayprogress?f=" + drmId,
            "type": "POST",
            "dataType": "json"
        },
        "columns": [{ "data": "ProgressDate", "name": "ProgressDate" },
        {
            "data": "Definition", "name": "Definition"

        },
        {
            "data": "Id", "render": function (data) {
                return "<button onclick='Update(" + data + ")' class='btn btn-light btn-sm'><i class='fa fa-pen'></i></button> | <button onclick='Remove(" + data + ")' class='btn btn-light btn-sm'><i class='fa fa-trash'></i></button>  "

            }
        }
        ],
        columnDefs: [{
            "targets": [0], "render": function (data, type) {


                if (data === null) {
                    return data;
                } else {
                    var s = new Date(parseInt(data.substr(6)));
                    return type === 'sort' ? data : dateToJs(s, 'datetime');
                }

            }
        }],
        "serverSide": true,
        "processing": true,
        "language": { "emptyTable": "No data found, Please click on <b>Add New</b> Button." },
        "info": false,
        "filter": false,
        "paging": false

    });

    $('#frmUpdateProgress input[name="Progress.ProgressDate"]').datetimepicker({
        widgetPositioning: {
            horizontal: 'auto',
            vertical: 'bottom'
        },
        format: 'MM/DD/YYYY HH:mm',
        icons: {
            time: "fa fa-clock"
        }
    });

    $('#frmProgress input[name="Progress.ProgressDate"]').datetimepicker({
        widgetPositioning: {
            horizontal: 'auto',
            vertical: 'bottom'
        },
        format: 'MM/DD/YYYY HH:mm',
        icons: {
            time: "fa fa-clock"
        }
    });





    $('input[name="Attachment.Attachment"]').change(function (event) {
        imagePreview.preview(event);
        let file = $(this);
        $('#remove-preview').click(function () {
            imagePreview.remove(file);

        });
    });

});

function CreateProgress() {
    event.preventDefault();
    let form = $('#frmProgress');

    var progressDate = $('#frmProgress input[name="Progress.ProgressDate"]').val();
    var strToDate = new Date(progressDate);
    if (form.valid()) {
        let model = {
            ProgressDate: strToDate,
            ProgressId: $('#frmProgress select[name="Progress.ProgressId"]>option:selected').val(),

            f: drmId
        };

        Save(model);

    }

}


function SaveUpdateProgress() {
    event.preventDefault();
    let form = $('#frmUpdateProgress');
    let progressDate = $('#frmUpdateProgress input[name="Progress.ProgressDate"]').val();
    let strProggressDate = new Date(progressDate);
    if (form.valid()) {

        let model = {
            Id: $('input[name="Progress.Id"]').val(),
            DateCreated: strProggressDate,
            DeliveryProgressId: $('#frmUpdateProgress select[name="Progress.ProgressId"]>option:selected').val(),
            f: drmId
        };

        SaveChanges(model);

    } 

}
function Update(progressId) {

    let model = {
        f: drmId,
        pId: progressId


    };

    //GetProgressById(model);
    getProgress(model).then((data) => {
        var s = new Date(parseInt(data.data.ProgressDate.substr(6)));

        $('#frmUpdateProgress input[name="Progress.ProgressDate"]').val(dateToJs(s, 'datetime'));

        $('#frmUpdateProgress select[name="Progress.ProgressId"]').val(data.data.ProgressId);
        $('input[name="Progress.Id"]').val(data.data.Id);
        $('#modalUpdateProgress').modal('show');


    }).catch((error) => {
        console.log(error);
    });
}



function Remove(progressId) {

    let model = {
        f: drmId,
        pId: progressId

    };

    getProgress(model).then((data) => {
        var s = new Date(parseInt(data.data.ProgressDate.substr(6)));

        $('#frmDeleteProgress #Date').text('Date: ' + dateToJs(s, 'datetime'));

        $('#frmDeleteProgress #Definition').text('Progress: ' + data.data.Definition);
        $('input[name="Progress.Id"]').val(data.data.Id);
        $('#modalDeleteProgress').modal('show');


    }).catch((error) => {
        console.log(error);
    });
}

function DeleteProgress(model) {
    $.ajax({
        type: 'POST',
        url: location.origin + '/drm/getprogress',
        data: JSON.stringify(model),
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        beforeSend: function () {

        }

    }).done(function (data) {
        var s = new Date(parseInt(data.data.ProgressDate.substr(6)));

        $('#frmUpdateProgress input[name="Progress.ProgressDate"]').val(dateToJs(s, 'date'));

        $('#frmUpdateProgress select[name="Progress.ProgressId"]').val('Progress: '+data.data.Definition);
        $('input[name="Progress.Id"]').val(data.data.Id);
        $('#modalDeleteProgress').modal('show');
    }).fail(function (jqXHR, textStatus, errorThrown) {
        alert(textStatus + ': ' + errorThrown);
    });
}
let btnUpdateProgress = new ButtonLoader('btnSaveUpdateProgress');

let SaveChanges = function (model) {

    $.ajax({
        type: 'POST',
        url: location.origin + '/drm/savechangestoprogress',
        data: JSON.stringify(model),
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        beforeSend: function () {
            btnUpdateProgress.Processing();
        }

    }).done(function (data) {
        btnUpdateProgress.Completed();
        btnUpdateProgress.Default();
        setTimeout(function () { alert(data.message);},1500);
    }).fail(function (jqXHR, textStatus, errorThrown) {
        alert(textStatus + ': ' + errorThrown);
    });

};
let btnSaveProgress = new ButtonLoader('btnSaveProgress');

let Save = function (model) {

    $.ajax({
        type: 'POST',
        url: location.origin + '/drm/saveprogress',
        data: JSON.stringify(model),
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        beforeSend: function () {
            btnSaveProgress.Processing();
        }

    }).done(function (data) {
        btnSaveProgress.Completed();
        setTimeout(function () { alert(data.message); }, 1500);
        btnSaveProgress.Default();
    }).fail(function (jqXHR, textStatus, errorThrown) {
        alert(textStatus + ': ' + errorThrown);
    });

};

let getProgress = function (model) {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: 'POST',
            url: location.origin + '/drm/getprogress',
            data: JSON.stringify(model),
            dataType: 'json',
            contentType: 'application/json;charset=utf-8',
            success: function (data) {
                resolve(data);
            },
            error: function (error) {
                reject(error);
            }
        });
    });
};

//x(model).then((data) => {
//    var s = new Date(parseInt(data.data.ProgressDate.substr(6)));

//    $('#frmUpdateProgress input[name="Progress.ProgressDate"]').val(dateToJs(s, 'datetime'));

//    $('#frmUpdateProgress select[name="Progress.ProgressId"]').val(data.data.ProgressId);
//    $('input[name="Progress.Id"]').val(data.data.Id);
//    $('#modalUpdateProgress').modal('show');


//}).catch((error) => {
//    console.log(error);
//});
