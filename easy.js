(function () {
    var path, i, ln, scriptSrc, match,
    scripts = document.getElementsByTagName('script');

    for (i = 0, ln = scripts.length; i < ln; i++) {
        scriptSrc = scripts[i].src;

        match = scriptSrc.match(/easy\.js$/);

        if (match) {
            path = scriptSrc.substring(0, scriptSrc.length - match[0].length);
            break;
        }
    }

    document.write('<link href="' + path + 'resources/css/all-mini.css" rel="stylesheet" type="text/css" />');
    document.write('<link href="' + path + 'theme/deepblue/css/all-mini.css" rel="stylesheet" type="text/css" />');
    
    document.write('<script type="text/javascript" src="' + path + 'Easy-base-mini.js"></script>');
    document.write('<script type="text/javascript" src="' + path + 'Easy-ui-mini.js"></script>');
    if (navigator.userAgent.indexOf("MSIE 6") > -1) {
        document.write('<link href="' + path + 'theme/ie6hack/css/all.css" rel="stylesheet" type="text/css" />');
    }
})();
