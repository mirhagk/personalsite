﻿window.addEventListener('load', function () {
    var links = Array.prototype.slice.call(document.getElementsByTagName("a"));
    links
        .filter(function (x) {
            x.linkSrc = x.attributes.href.value;
            return x.linkSrc.length > 0 && x.linkSrc[0] == "/";
        })
        .forEach(function (x) {
            x.attributes.href.value = "javascript:void(0);";
            x.onclick = function (event) { renderPartial(x.linkSrc,this,event); };
        });
}, false);
window.onpopstate = function (event) {
    window.location.href = document.location;
}
renderPartial = function (src, e, evt) {
    history.pushState(null, null, src);

    var request = new XMLHttpRequest();
    request.open("GET", "/Partial/Render"+src, false);
    request.onload = function () {
        //if the result was not successful
        if (this.status != 200) {
            //then navigate to the page regularly
            //this is a failsafe in case the server side partial rendering implementation doesn't work
            window.location.href = document.location;
            return;
        }
        document.getElementById('main').innerHTML = this.responseText;
    }
    request.onerror = function () {
        window.location.href = src;
    }
    request.send();

    evt.preventDefault();
    return false;
};