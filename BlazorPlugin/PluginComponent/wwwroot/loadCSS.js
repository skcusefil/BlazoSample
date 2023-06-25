

export function addComponentCSS() {

    let id = "component-css";

    var cssLink = document.createElement('link');
    cssLink.setAttribute('id', id);
    cssLink.setAttribute('rel', 'stylesheet');
    cssLink.setAttribute('type', 'text/css');
    cssLink.setAttribute('href', '/Plugins/PluginComponent/wwwroot/componentCSS.css');
    document.head.appendChild(cssLink);

}