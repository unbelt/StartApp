(function () {
    'use strict';

    // target _blank to all external links
    var links = document.getElementsByTagName('a');

    [].forEach.call(links, function (link) {
        if (link.getAttribute('href').indexOf(location.hostname) === -1) {
            link.setAttribute('target', '_blank');
        }
    });

}());
