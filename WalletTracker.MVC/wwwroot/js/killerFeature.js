// Show category limits, money spent and calculate money left
$(function () {
    $(window).on("load", async function () {
        await manageLimit();
        showMoneyLeft();
    })

    $("#CategoryId, #ExpenseDate").on("change", async function () {
        await manageLimit();
        showMoneyLeft();
    });

    $("#Amount").on("input", function () {
        showMoneyLeft();
    });

    // Call limit methods
    const manageLimit = async () => {
        let categoryId = $("#CategoryId").val();
        let date = $("#ExpenseDate").val();
        await showCategoryLimit(categoryId);
        await showMoneySpent(categoryId, date);
    }

    // Show category limit by id
    const showCategoryLimit = async (categoryId) => {
        if (categoryId == 0) {
            $("#limitInfo").text("Category required");
            hideElements();
        } else {
            try {
                var data = await $.ajax({
                    url: `/Expense/GetCategoryLimit/${categoryId}`,
                    type: "get"
                });

                if (!data["limitIsActive"]) {
                    $("#limitInfo").text("No limit set");
                    hideElements();
                } else {
                    let limit = Number(data["limit"]).toFixed(2);
                    $("#limitInfo").text(`${limit.replace(/\./g, ',')} PLN`);
                }

            } catch (error) {
                toastr["error"]("Something went wrong");
            }
        }
    }

    // Show spent money in defined month
    const showMoneySpent = async (categoryId, date) => {
        if ((categoryId == 0) || (date == "")) {
            $("#moneySpent").text("Category & date required");
        } else {
            let limitInfo = $("#limitInfo").text();

            if (limitInfo != "No limit set") {
                try {
                    const data = await $.ajax({
                        url: `/Expense/GetMoneySpent/${categoryId}/${date}`,
                        type: "get"
                    });

                    let moneySpent = Number(data).toFixed(2);
                    $("#moneySpent").text(`${moneySpent.replace(/\./g, ',')} PLN`);
                    $(".moneySpentRow").removeClass("hide-class");

                } catch (error) {
                    toastr["error"]("Something went wrong");
                }
            }
        }
    }

    // Show left money in defined month
    const showMoneyLeft = () => {
        let categoryId = $("#CategoryId").val();
        let date = $("#ExpenseDate").val();
        let amount = $("#Amount").val();

        if ((categoryId == 0) || (date == "") || (amount == "")) {
            $("#moneyLeft").removeClass("moneyLeftPlus");
            $("#moneyLeft").removeClass("moneyLeftMinus");
            $("#moneyLeft").text("Category, date & amount required");
        } else {
            let limitInfo = $("#limitInfo").text();
            let moneySpent = $("#moneySpent").text();

            if (limitInfo != "No limit set") {
                limitInfo = limitInfo.replace(/[^0-9\,]/g, "").replace(",", ".");
                moneySpent = moneySpent.replace(/[^0-9\,]/g, "").replace(",", ".");
                amount = amount.replace(",", ".");

                moneyLeft = limitInfo - moneySpent - amount;

                if (moneyLeft >= 0) {
                    $("#moneyLeft").addClass("moneyLeftPlus");
                } else {
                    $("#moneyLeft").addClass("moneyLeftMinus");
                }

                $("#moneyLeft").text(`${moneyLeft.toFixed(2)} PLN`);
                $(".moneyLeftRow").removeClass("hide-class");
            }  
        }
    }

    const hideElements = () => {
        $(".moneySpentRow").addClass("hide-class");
        $(".moneyLeftRow").addClass("hide-class");
    }
});
