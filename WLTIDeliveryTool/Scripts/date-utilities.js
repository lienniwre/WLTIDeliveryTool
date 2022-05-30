function dateToJs(cDate,format) {
    let date_ob = new Date(cDate);

    // adjust 0 before single digit date
    let date = ("0" + date_ob.getDate()).slice(-2);

    // current month
    let month = ("0" + (date_ob.getMonth() + 1)).slice(-2);

    // current year
    let year = date_ob.getFullYear();

    // current hours
    let hours = ("0" + date_ob.getHours()).slice(-2);

    // current minutes
    let minutes = ("0" + date_ob.getMinutes()).slice(-2);

    // current seconds
    let seconds = ("0" + date_ob.getSeconds()).slice(-2);
    if (format === 'datetime') {
        // prints date & time in YYYY-MM-DD HH:MM:SS format
        return month + "/" + date + "/" + year + " " + hours + ":" + minutes + ":" + seconds;
    } else if (format === 'date') {
        // prints date & time in YYYY-MM-DD HH:MM:SS format
        return month + "/" + date + "/" + year;
    } else {
        return month + "/" + date + "/" + year + " " + hours + ":" + minutes + ":" + seconds;
    }
    
}

function getDay(cDate) {
    let date_ob = new Date(cDate);

    // adjust 0 before single digit date
    let date = ("0" + date_ob.getDate()).slice(-2);

    // current month
    let month = ("0" + (date_ob.getMonth() + 1)).slice(-2);

    // current year
    let year = date_ob.getFullYear();

    // current hours
    let hours = date_ob.getHours();

    // current minutes
    let minutes = date_ob.getMinutes();

    // current seconds
    let seconds = date_ob.getSeconds();

    let currentDate = new Date();
    if (date === currentDate.getDate()) {
        return 'Today';
    } else {
        return StrMonth(date_ob.getMonth()) +', '+date;
    }

    function StrMonth(intMonth) {
        var month = new Array();
        month[0] = "January";
        month[1] = "February";
        month[2] = "March";
        month[3] = "April";
        month[4] = "May";
        month[5] = "June";
        month[6] = "July";
        month[7] = "August";
        month[8] = "September";
        month[9] = "October";
        month[10] = "November";
        month[11] = "December";
        return month[intMonth];
    }

    // prints date & time in YYYY-MM-DD HH:MM:SS format
    //return month + "/" + date + "/" + year + " " + hours + ":" + minutes + ":" + seconds;
}
function GetTime(cDate) {
    let date_ob = new Date(cDate);

    // adjust 0 before single digit date
    let date = ("0" + date_ob.getDate()).slice(-2);

    // current month
    let month = ("0" + (date_ob.getMonth() + 1)).slice(-2);

    // current year
    let year = date_ob.getFullYear();

    // current hours
    let hours = ("0" + date_ob.getHours()).slice(-2);

    // current minutes
    let minutes = ("0" + date_ob.getMinutes()).slice(-2);

    // current seconds
    let seconds = ("0" + date_ob.getSeconds()).slice(-2);

    // prints date & time in YYYY-MM-DD HH:MM:SS format
    return hours + ":" + minutes;
}