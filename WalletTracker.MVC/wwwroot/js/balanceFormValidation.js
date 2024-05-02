// Validate form responsible for choosing dates
$("#balanceForm").validate({
    rules: {
        StartDate: {
            dateTime: true,
            min: "2000-01-01",
            dateNotGreaterThanToday: true
        },
        EndDate: {
            dateTime: true,
            min: "2000-01-01",
            dateNotGreaterThanToday: true,
            endDateGreaterThanStartDate: true
        }
    },
    messages: {
        StartDate: {
            min: "Please enter a date equal or greater than 01-01-2000."
        },
        EndDate: {
            min: "Please enter a date equal or greater than 01-01-2000."
        }
    },
    errorPlacement: function (error, element) {
        if ($(element).attr("name") == "StartDate")
            $("#startDateSpan").text(error.text());
        else if ($(element).attr("name") == "EndDate")
            $("#endDateSpan").text(error.text());
    },
    success: function (label, element) {
        if ($(element).attr("name") == "StartDate")
            $("#startDateSpan").text("");
        else if ($(element).attr("name") == "EndDate")
            $("#endDateSpan").text("");
    }
});