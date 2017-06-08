
notification = function (num) {
    $('#notify').empty();
    $('#notify').append("<span class='badge badge-notify'>" + num + "</span>");
}

notifyFriendRequest = function (num) {
    $('#notifyFriend').empty();
    if (num > 0) {
        $('#notifyFriend').append("<span class='badge badge-notify'>" + num + "</span>");

    }

}


zp = function (username) {
    $('#reqMess').empty();
    $('#reqMess').append("<button id='msg' value='"+username+"' class='btn btn-primary btn-success'>Send Message &nbsp;<span class='glyphicon glyphicon-user'></span></button>");
} //nesto ne radi


MakeChart = function () {
    var chart = new CanvasJS.Chart("chartContainer",
    {
        title: {
            text: "Statistika"
        },
        animationEnabled: true,
        legend: {
            verticalAlign: "center",
            horizontalAlign: "left",
            fontSize: 20,
            fontFamily: "Helvetica"
        },
        theme: "theme2",
        data: [
        {
            type: "pie",
            indexLabelFontFamily: "Garamond",
            indexLabelFontSize: 20,
            indexLabel: "{label} {y}",
            startAngle: -20,

            toolTipContent: "{legendText} {y}",
            dataPoints: []
        }
        ]
    });
    return chart;
}

    start = function () {
        chart.render();
    }



