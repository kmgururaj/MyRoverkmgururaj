$(document).ready(function () {

    var timerHandle;
    var maxRows = numberOfRows;
    var maxColumns = numberOfColumns;
    var timeInterval = communicationLatenceInMilliseconds;

    $(function () {
        $("#btnGet").click(handelButtonClick);
    });

    var handelButtonClick = function () {
        renderTable();
        getAllRoversInitialData();
    };

    var renderTable = function () {
        var table = $('<table></table>').addClass('table table-bordered');
        for (var i = 0; i < maxRows; i++) {
            var row = $('<tr></tr>').addClass('tr');
            for (var j = 0; j < maxColumns; j++) {
                var row1 = $('<td ></td>').addClass('td').text(' ');
                table.append(row);
                row.append(row1);
            }
        }
        $('#RootDiv').html(table);
    }

    var moveRover = function (currentX, currentY, roverText) {
        var currentTr = $('#RootDiv tr').eq(currentY);
        if (currentTr.length > 0) {
            var currentTd = currentTr.find('td').eq(currentX);
            if (currentTd.length > 0) {
                currentTd.addClass('td warning').text(roverText);
            }
        }
    }

    var getAllRoversInitialData = function () {
        $.ajax({
            type: "GET",
            url: "/Home/GetAllRoversInitialData",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: processGetAllRoversInitialData,
            failure: log,
            error: log
        });
    }

    var processGetAllRoversInitialData = function (response) {

        for (var k = 0; k < response.length; k++) {
            var currentRover = response[k];
            var currentX = currentRover.CurrentX;
            var currentY = maxRows - 1 - currentRover.CurrentY;

            moveRover(currentX, currentY, currentRover.RoverText);
        }

        getNextCommands();
    };

    var getNextCommands = function () {
        stopTimer();

        $.ajax({
            type: "GET",
            url: "/Home/GetNextCommand",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: processGetNextCommand,
            failure: log,
            error: log
        });

    }

    var processGetNextCommand = function (response) {
        var length = response.length;
        for (var k = 0; k < length; k++) {
            var currentRover = response[k];
            var currentX = currentRover.CurrentX;
            var currentY = maxRows - 1 - currentRover.CurrentY;
            var previousY = maxRows - 1 - currentRover.PerviousY;
            var previousX = currentRover.PerviousX;

            var previousTr = $('#RootDiv tr').eq(previousY);
            if (previousTr.length > 0) {
                var previousTd = previousTr.find('td').eq(previousX);
                if (previousTd.length > 0) {
                    previousTd.removeClass('td active warning').text('');
                }
            }

            var currentTr = $('#RootDiv tr').eq(currentY);
            if (currentTr.length > 0) {
                var currentTd = currentTr.find('td').eq(currentX);
                if (currentTd.length > 0) {
                    currentTd.addClass('td active').text(currentRover.RoverText);
                }
            }
        }

        //if no commands turn off timer
        if (length > 0) {
            startTimer()
        }

    }

    var startTimer = function () {
        timerHandle = setInterval(getNextCommands, timeInterval);
    }

    var stopTimer = function () {
        clearInterval(timerHandle);
    }

    var log = function (response) {
        console.log(response);
    }

});
