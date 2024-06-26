// Validate form responsible for adding new income
$("#incomeForm").validate({
    rules: {
        Amount: {
            required: true,
            amountMin: true,
            amountMax: true,
            decimalPlaces: true
        },
        IncomeDate: {
            dateTime: true,
            min: "2000-01-01",
            dateNotGreaterThanToday: true
        },
        CategoryId: {
            required: true,
            min: 1
        },
        Comment: {
            maxlength: 25
        }
    },
    messages: {
        IncomeDate: {
            min: "Please enter a date equal or greater than 01-01-2000."
        },
        CategoryId: {
            min: "This field is required."
        },
        Comment: {
            maxlength: "This field contain less than 26 characters."
        }
    },
    errorPlacement: function (error, element) {
        if ($(element).attr("name") == "Amount")
            $("#amountSpan").text(error.text());
        else if ($(element).attr("name") == "IncomeDate")
            $("#incomeDateSpan").text(error.text());
        else if ($(element).attr("name") == "CategoryId")
            $("#categoryIdSpan").text(error.text())
        else if ($(element).attr("name") == "Comment")
            $("#commentIdSpan").text(error.text());
    },
    success: function (label, element) {
        if ($(element).attr("name") == "Amount")
            $("#amountSpan").text("");
        else if ($(element).attr("name") == "IncomeDate")
            $("#incomeDateSpan").text("");
        else if ($(element).attr("name") == "CategoryId")
            $("#categoryIdSpan").text("")
        else if ($(element).attr("name") == "Comment")
            $("#commentIdSpan").text("");
    }
});