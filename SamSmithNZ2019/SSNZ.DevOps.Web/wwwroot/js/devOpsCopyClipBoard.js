function selectElementContentsById(elementName) {

    var el = document.getElementById(elementName);

    // Copy textarea, pre, div, etc.
    if (document.body.createTextRange) {
        // IE
        var textRange = document.body.createTextRange();
        textRange.moveToElementText(el);
        textRange.select();
        textRange.execCommand("Copy");
    }
    else if (window.getSelection && document.createRange) {
        // non-IE
        var range = document.createRange();
        range.selectNodeContents(el);
        var sel = window.getSelection();
        sel.removeAllRanges();
        sel.addRange(range);
        try {
            var successful = document.execCommand('copy');
            var msg = successful ? 'successful' : 'unsuccessful';
            console.log('Copy command was ' + msg);
        } catch (err) {
            console.log('Oops, unable to copy');
        }
    }
} // end function selectElementContents(el)
