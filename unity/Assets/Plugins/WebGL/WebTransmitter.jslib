var WebTransmitter = {

    SendData: function(str)
    {
        var processedStr = Pointer_stringify(str);
        var data = new Blob([processedStr], {type: "text/plain"});

        var link = document.createElement("a");
        link.download = "TransmittedFile.txt";
        link.intterHTML = "Download File";

        if(window.webkitURL != null)
        {
            link.href = window.webkitURL.createObjectURL(data);
        }
        else
        {
            link.href = window.URL.createObjectURL(data);
            link.onclick = function() { 
                document.body.removeChild(event.target);
            };
            link.style.display = "none";
            document.body.appendChild(link);
        }

        link.click();
    }
}

mergeInto(LibraryManager.library, WebTransmitter);
