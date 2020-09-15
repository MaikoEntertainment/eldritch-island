mergeInto(LibraryManager.library, {
  DownloadJson: function (saveFileJson) {
    var downloadAnchorNode = document.createElement("a");
    downloadAnchorNode.setAttribute("href", saveFileJson);
    downloadAnchorNode.setAttribute("download", "SOUL.json");
    document.body.appendChild(downloadAnchorNode); // required for firefox
    downloadAnchorNode.click();
    downloadAnchorNode.remove();
  },
});
