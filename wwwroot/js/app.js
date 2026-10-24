window.clipboardHelper = {
    copyText: function (text, buttonId) {
        navigator.clipboard.writeText(text).then(function () {
            var btn = document.getElementById(buttonId);
            if (btn) {
                var originalText = btn.innerHTML;
                btn.innerHTML = '<svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><polyline points="20 6 9 17 4 12"></polyline></svg> Copied!';
                btn.classList.add('copied');
                setTimeout(function () {
                    btn.innerHTML = originalText;
                    btn.classList.remove('copied');
                }, 2000);
            }
        }).catch(function (err) {
            console.error('Failed to copy: ', err);
        });
    }
};

window.progressStorage = {
    saveState: function (key, dataJson) {
        try {
            localStorage.setItem('revit_academy_' + key, dataJson);
        } catch (e) {
            console.error(e);
        }
    },
    loadState: function (key) {
        try {
            return localStorage.getItem('revit_academy_' + key);
        } catch (e) {
            return null;
        }
    }
};

window.downloadHelper = {
    downloadFromBase64: function (fileName, contentType, base64Data) {
        try {
            var link = document.createElement('a');
            link.download = fileName;
            link.href = "data:" + contentType + ";base64," + base64Data;
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        } catch (err) {
            console.error("Download failed:", err);
        }
    }
};
