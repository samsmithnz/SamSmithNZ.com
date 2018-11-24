function updateTFSVSTSScript(oldElementLIId, newElementLIId, oldElementDivId, newElementDivId) {

    var oldElTab = $("#" + oldElementLIId);
    var newElTab = $("#" + newElementLIId);
    var oldElBody = $("#" + oldElementDivId);
    var newElBody = $("#" + newElementDivId);
    
    if (newElTab.hasClass("action") == false) {
        oldElTab.toggleClass("active")
        newElTab.toggleClass("active")
    }

    if (newElBody.hasClass("in active") == false) {
        oldElBody.toggleClass("in active")
        newElBody.toggleClass("in active")
    }

} // end function updateTFSVSTSScript(oldElementLIId, newElementLIId, oldElementDivId, newElementDivId)
