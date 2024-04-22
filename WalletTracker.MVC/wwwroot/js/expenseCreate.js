// Validate form responsible for adding new income
$("#expenseForm").validate({
    rules: {
        Amount: {
            required: true,
            amountMin: true,
            amountMax: true,
            decimalPlaces: true
        },
        ExpenseDate: {
            dateTime: true,
            min: "2000-01-01",
            dateNotGreaterThanToday: true
        },
        PaymentId: {
            required: true,
            min: 1
        },
        CategoryId: {
            required: true,
            min: 1
        }
    },
    messages: {
        ExpenseDate: {
            min: "Please enter a date equal or greather than 01-01-2000."
        },
        PaymentId: {
            min: "This field is required."
        },
        CategoryId: {
            min: "This field is required."
        }
    },
    errorPlacement: function (error, element) {
        if (element.attr("name") == "Amount")
            $("#amountSpan").text(error.text());
        else if (element.attr("name") == "ExpenseDate")
            $("#expenseDateSpan").text(error.text());
        else if (element.attr("name") == "PaymentId")
            $("#paymentIdSpan").text(error.text());
        else if (element.attr("name") == "CategoryId")
            $("#categoryIdSpan").text(error.text());
    },
    success: function (label, element) {
        if ($(element).attr("name") == "Amount")
            $("#amountSpan").text("");
        else if (element.attr("name") == "IncomeDate")
            $("#incomeDateSpan").text("");
        else if (element.attr("name") == "PaymentId")
            $("#paymentIdSpan").text("");
        else if (element.attr("name") == "CategoryId")
            $("#categoryIdSpan").text("");
    }
});