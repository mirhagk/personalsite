var load_event = document.createEvent("HTMLEvents");
load_event.initEvent("load", true, true);
window.addEventListener('load', function () {
    var links = Array.prototype.slice.call(document.getElementsByTagName("a"));
    links
        .filter(function (x) {
            if (x.linkSrc)//if it's already been processed, skip it
                return false;
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
createLoadingMask = function(){
    var el = document.createElement('div');
    el.id="loadingMask";
    return el;
}
loadMask = function () {
    var hiddenElement = document.getElementById('#loadingMask') || createLoadingMask();
    hiddenElement.style.display = "none";
}
hideLoadMask = function () {

}
renderPartial = function (src, e, evt) {
    loadMask();
    history.pushState(null, null, src);

    var request = new XMLHttpRequest();
    var link = src + ((src.indexOf("?") == -1) ? "?" : "&") + "partial=true";
    request.open("GET", link, false);
    request.onload = function () {
        //if the result was not successful
        if (this.status != 200) {
            //then navigate to the page regularly
            //this is a failsafe in case the server side partial rendering implementation doesn't work
            window.location.href = document.location;
            hideLoadMask();
            return;
        }
        document.getElementById('main').innerHTML = this.responseText;
        window.dispatchEvent(load_event);
        hideLoadMask();
        
    }
    request.onerror = function () {
        window.location.href = src;
        hideLoadMask();
    }
    request.send();

    evt.preventDefault();
    return false;
};