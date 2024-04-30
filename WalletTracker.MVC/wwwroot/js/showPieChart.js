$(function () {
    const colorSet = [];

    for (i = 0; i < groupedExpenses.length; i++) {
        const r = Math.floor(Math.random() * 255);
        const g = Math.floor(Math.random() * 255);
        const b = Math.floor(Math.random() * 255);

        colorSet.push("rgba(" + r + ", " + g + ", " + b + ", 0.8)");
    }

    // Pie chart - expenses in defined period
    window.onload = function () {

        CanvasJS.addColorSet("randomSet", colorSet);

        var chart = new CanvasJS.Chart("chartContainer", {
            animationEnabled: true,
            backgroundColor: "transparent",
            colorSet: "randomSet",
            data: [{
                type: "pie",
                indexLabelFontColor: "black",
                indexLabelFontSize: 10,
                startAngle: 240,
                yValueFormatString: "##0.00\" PLN\"",
                indexLabel: "{label} {y}",
                dataPoints: groupedExpenses
            }]
        });
        chart.render();
    }
})