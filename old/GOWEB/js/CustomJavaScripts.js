
$(document).ready(function () {
    $("div.fancyDemo a").fancybox();
});


$(function () {

    var filterList = {

        init: function () {

            // MixItUp plugin
            // http://mixitup.io
            $('#portfoliolist').mixitup({
                targetSelector: '.portfolio',
                filterSelector: '.filter',
                effects: ['fade'],
                easing: 'snap',
                // call the hover effect
                onMixEnd: filterList.hoverEffect()
            });

        },

        hoverEffect: function () {

            // Simple parallax effect
            $('#portfoliolist .portfolio').hover(function () {
                $(this).find('.label').stop().animate({
                    bottom: 0
                }, 200, 'easeOutQuad');
                $(this).find('img').stop().animate({
                    top: -30
                }, 500, 'easeOutQuad');
            }, function () {
                $(this).find('.label').stop().animate({
                    bottom: -40
                }, 200, 'easeInQuad');
                $(this).find('img').stop().animate({
                    top: 0
                }, 300, 'easeOutQuad');
            });

        }
    };

    // Run the show!
    filterList.init();

});







//start circle

(function ($) {
    $.fn.percentPie = function (options) {

        var settings = $.extend({
            width: 100,
            trackColor: "EEEEEE",
            barColor: "E2534B",
            barWeight: 30,
            startPercent: 0,
            endPercent: 1,
            fps: 60
        }, options);

        this.css({
            width: settings.width,
            height: settings.width
        });

        var that = this,
            hoverPolice = false,
            canvasWidth = settings.width,
            canvasHeight = canvasWidth,
            id = $('canvas').length,
            canvasElement = $('<canvas id="' + id + '" width="' + canvasWidth + '" height="' + canvasHeight + '"></canvas>'),
            canvas = canvasElement.get(0).getContext("2d"),
            centerX = canvasWidth / 2,
            centerY = canvasHeight / 2,
            radius = settings.width / 2 - settings.barWeight / 2;
        counterClockwise = false,
            fps = 1000 / settings.fps,
            update = .01;
        this.angle = settings.startPercent;

        this.drawArc = function (startAngle, percentFilled, color) {
            var drawingArc = true;
            canvas.beginPath();
            canvas.arc(centerX, centerY, radius, (Math.PI / 180) * (startAngle * 360 - 90), (Math.PI / 180) * (percentFilled * 360 - 90), counterClockwise);
            canvas.strokeStyle = color;
            canvas.lineWidth = settings.barWeight;
            canvas.stroke();
            drawingArc = false;
        }

        this.fillChart = function (stop) {
            var loop = setInterval(function () {
                hoverPolice = true;
                canvas.clearRect(0, 0, canvasWidth, canvasHeight);

                that.drawArc(0, 360, settings.trackColor);
                that.angle += update;
                that.drawArc(settings.startPercent, that.angle, settings.barColor);

                if (that.angle > stop) {
                    clearInterval(loop);
                    hoverPolice = false;
                }
            }, fps);
        }

        this.mouseover(function () {
            if (hoverPolice == false) {
                that.angle = settings.startPercent;
                that.fillChart(settings.endPercent);
            }
        });

        this.fillChart(settings.endPercent);
        this.append(canvasElement);
        return this;
    }
}(jQuery));

$(document).ready(function () {

    $('.google').percentPie({
        width: 100,
        trackColor: "E2534B",
        barColor: "76C7C0",
        barWeight: 20,
        endPercent: .9,
        fps: 60
    });

    $('.moz').percentPie({
        width: 100,
        trackColor: "E2534B",
        barColor: "76C7C0",
        barWeight: 20,
        endPercent: .75,
        fps: 60
    });

    $('.safari').percentPie({
        width: 100,
        trackColor: "E2534B",
        barColor: "#76C7C0",
        barWeight: 20,
        endPercent: .5,
        fps: 60
    });

});
