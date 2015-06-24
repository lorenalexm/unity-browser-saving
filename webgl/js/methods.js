function saveSceneToDisk(str) {
    var data = new Blob([str], {type: "text/xml"});
    var link = document.createElement("a");
    link.download = "SceneCollection.xml";
    link.innerHTML = "Download File";
    if(window.webkitURL != null) {
        link.href = window.webkitURL.createObjectURL(data);
    } else {
        link.href = window.URL.createObjectURL(data);
        link.onclick = function() {
            document.body.removeChild(event.target);
        };
        link.style.display = "none";
        document.body.appendChild(link);
    }
    link.click();
};

function loadAndTransmit(file) {
    var loadedFile = file;
    var reader = new FileReader();
    reader.onload = function(data) {
        SendMessage("Manager", "OnReceiveData", data.target.result)
    }
    reader.readAsText(loadedFile);
};
