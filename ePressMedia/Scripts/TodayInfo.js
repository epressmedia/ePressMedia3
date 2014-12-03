$(document).ready(function () {
    var myDate = new Date();
    var weekName = new Array("일", "월", "화", "수", "목", "금", "토");
    var myDay = myDate.getDay();
    var myMonth = myDate.getMonth();
    $('.TodayInfo_Header').text(myDate.getFullYear() + "." + (myMonth + 1) + "." + myDate.getDate() + " (" + weekName[myDay] + ")"); // ;
});