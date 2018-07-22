var response = [];

$.ajax({
    url: "/Coordinates/GetAll",
    success: function (data) {

        response = data;
        if (response.length > 2) {

            var arr = [response];

            var margin = { top: 20, right: 20, bottom: 30, left: 50 },
                width = 400 - margin.left - margin.right,
                height = 400 - margin.top - margin.bottom;

            var svg = d3.select("#barChart").append("svg")
                .attr("width", width + margin.left + margin.right)
                .attr("height", height + margin.top + margin.bottom)
                .append("g")
                .attr("transform", "translate(" + margin.left + "," + margin.top + ")");


            var x = d3.scaleLinear().range([0, 100]);
            var y = d3.scaleLinear().range([100, 0]);

            x.domain([0, 10]);
            y.domain([0, 10]);

            svg.selectAll("polygon")
                .data(arr)
                .enter().append("polygon")
                .attr("points", function (d) {
                    return d.map(function (d) {
                        return [x(d.X), y(d.Y)].join(",");
                    }).join(" ");
                });

            // add the X Axis
            svg.append("g")
                .attr("transform", "translate(" + 0 + "," + 100 + ")")
                .call(d3.axisBottom(x));

            // add the Y Axis
            svg.append("g")
                .call(d3.axisLeft(y));
        }
    }
});

