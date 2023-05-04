let tooltipCount = 0;

$("body").tooltip({
    selector: '[data-toggle=tooltip]', trigger: 'focus', html: true,
    template: '<div class="tooltip" role="tooltip"><div class="tooltip-arrow"></div><div class="tooltip-inner">',
    title: '<h6>Совет!</h6>Доколку дијагнозата која ќе ја запишете не е во листата погоре, истата ќе биде зачувана за понатамошна употреба.</div></div>'
});
$('#diagnosisValue').on('focus', function () {
    let that = $(this);
    if (tooltipCount < 2) {
        that.tooltip('show');
        setTimeout(function () {
            that.tooltip('hide');
        }, 4000);
        tooltipCount++;
    } else {
        that.tooltip('hide');
    }

})