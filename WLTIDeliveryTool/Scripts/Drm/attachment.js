$(document).ready(function () {
    $('#tblAttachment').DataTable({
        "ajax": {
            "url": "/Attachment/DisplayAttachments?f=" + drmId,
            "type": "POST",
            "dataType": "json"
        },
        "columns": [{ "data": "FileName", "name": "FileName" },
        {
            "data": "Attachment", "render": function (data) {
                if (data !== null) {
                    return "<img src='data:Image/png;base64," + data + "' width='auto' style='object-fit:cover;max-height:85px;' />"
                } else {
                    return "";
                }

            },
            "orderable": false
        },
        { "data": "FileType", "name": "FileType", "width": "120px;" },
        {
            "data": "Id", "render": function (data) {

                return "<button onclick='DisplayAttachment(" + data + ");' class='btn btn-light btn-sm'><i class='fa fa-search'></i></button>|<button onclick='DeleteAttachment(" + data + ")' class='btn btn-light btn-sm'><i class='fa fa-trash'></i></button>"
            },
            "orderable": false,
            "width": "200px"
        }
        ],
        "serverSide": true,
        "processing": true,
        "language": { "emptyTable": "No data found, Please click on <b>Add New</b> Button." },
        "info": false,
        "filter": false,
        "paging": false


    });

    $('input[name="Attachment.Attachment"]').change(function (e) {
        let file = e.target.files[0].name;
        $('input[name="Attachment.FileName"]').val(file);
       
    });
   
});

function DeleteAttachment(id) {

    let model = {
        id: id
    };
    GetAttachmentDetails(model, 'delete');

    $('#deleteAttachment').css('display', 'block');
    $('#deleteAttachment').attr('disabled', false);
    $('#modalUpdateAttachment').modal('show');
}

function GetAttachmentDetails(aid, action) {
    $.ajax({
        url: location.origin + '/Attachment/displayAttachment',
        type: 'POST',
        data: JSON.stringify(aid),
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        beforeSend: function () {


        },
        success: function (data) {
            if (data.returnCode === 200) {

                let imgsrc = 'data:' + data.data.FileType + ';base64,' + data.data.StrAttachment;
                let image = '<img src="' + imgsrc + '" class="mb-3" style="max-height:100%;max-width:100%;object-fit:cover;" />'
                $('.display-image').empty();
                $('.display-image').append(image);
                $('input[name="Attachment.F"]').val(data.data.F);
                if (action === 'delete') {
                    $('#modalLabelFileName').html('Delete ' + data.data.FileName + '?');

                } else {
                    $('#modalLabelFileName').html(data.data.FileName);

                }

            } else {
                alert('Oops something went wrong');
            };

        }

    });
}

function DisplayAttachment(id) {

    let model = {
        id: id
    };
    GetAttachmentDetails(model, 'display');
    $('#deleteAttachment').hide();
    $('#deleteAttachment').attr('disabled', true);
    $('#modalUpdateAttachment').modal('show');
}

function ConfirmDelete() {
    event.preventDefault();


    let model = {
        f: $('input[name="Attachment.F"]').val()

    };



    $.ajax({
        type: 'POST',
        url: location.origin + '/attachment/remove',
        data: JSON.stringify(model),
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        beforeSend: function () {

        }

    }).done(function (data) {
        alert(data.message);
    }).fail(function (jqXHR, textStatus, errorThrown) {
        alert(textStatus + ': ' + errorThrown);
    });

}
let btnSaveAttachment = new ButtonLoader('btnSaveAttachment')
function submit() {
    let form = $('#frmAddAttachment');
    event.preventDefault();
    if (form.valid()) {

        event.preventDefault();
        var formData = new FormData(form[0]);
        formData.append('F', drmId);

        for (var p of formData) {
            let name = p[0];
            let value = p[1];

            console.log(name, value);
        }

        $.ajax({
            url: location.origin + '/Attachment/SaveAttachment',
            type: 'POST',
            data: formData,
            async: false,
            beforeSend: function () {

                btnSaveAttachment.Processing();
            },
            success: function (data) {
                if (data.returnCode === 200) {
                    btnSaveAttachment.Completed();
                    btnSaveAttachment.Default();
                    setTimeout(function () { alert("success"); }, 1500);
                } else if (data.statusCode === 300) {
                    $('#file-type-validation').html('Attached file/s invalid');

                } else {
                    alert('Oops something went wrong');
                }

            },
            cache: false,
            contentType: false,
            processData: false
        });

        return false;


    }



};

let imagePreview = {
    'container': $('.preview-container'),
    'preview': function ShowPreview(event) {

        this.container.empty();
        if (event.target.files.length > 0) {
            var src = URL.createObjectURL(event.target.files[0]);


            this.container.append('<div class="card"><div class="card-header p-2"><i class="fa fa-times float-right" id="remove-preview" style="cursor:pointer;"></i></div><div class="card-body"><img src="' + src + '" class="mb-3" style="max-height:250px;max-width:250px;object-fit:cover;"/></div></div>');

        } else {

            this.container.empty();
        }
    },
    'remove': function (selector) {


        selector.val("");

        this.container.empty();

    },
    'displayOriginal': function (src) {

        this.container.empty();
        this.container.append('<center><img src=' + src + ' style="padding:5px;  max-height:250px;max-width250px;" class="image-container mb-5" /></center>');
    }
};
