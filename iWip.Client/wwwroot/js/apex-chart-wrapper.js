/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

window.apex_wrapper = {
    charts: [],

    renderApexChart(id, options) {
        var chart = new ApexCharts(document.querySelector("#" + id), options);
        chart.render();

        this.charts.push(chart);
    },
    
    updateSeries(id, series) {
        var chartFind = this.charts.filter(x => x.opts.chart.id === id)
        
        if (chartFind.length > 0) {
            var chart = chartFind[0];
            console.log(chart)
            chart.updateSeries(series);
        }
    },

     updateOptions(id, options) {
        var chartFind = this.charts.filter(x => x.opts.chart.id === id)

        if (chartFind.length > 0) {
            var chart = chartFind[0];
            console.log('updateOptions', chart)
            chart.updateOptions(options);
        }
    }
}

    
