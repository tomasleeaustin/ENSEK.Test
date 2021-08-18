import React, { useEffect, useRef, useState } from "react";

import "./accounts.scss";

// Prevent file drops from loading a new tab if the drop area is missed.
window.addEventListener("dragover", event => {
    event.preventDefault();
    event.stopPropagation();
});
window.addEventListener("drop", event => {
    event.preventDefault();
    event.stopPropagation();
});

const Accounts = props => {
    const defaultCsvDropClassName = "accounts-areas__upload-csv";

    const fileInputRef = useRef(null);

    const [csvDropClassName, setCsvDropClassName] = useState(defaultCsvDropClassName);
    const [errorResponse, setErrorResponse] = useState(null);

    const dragLeaveHandler = event => {
        setCsvDropClassName(defaultCsvDropClassName);
    };

    const dragOverHandler = event => {
        setCsvDropClassName(`${defaultCsvDropClassName} dragover`);
    };

    const dropHandler = event => {
        setCsvDropClassName(defaultCsvDropClassName);

        if (event.dataTransfer.files == null || event.dataTransfer.files.length === 0) {
            alert("Please drag and drop a csv file into the drop area");

            return;
        }

        uploadCsvFile(event.dataTransfer.files[0]);
    };

    const uploadCsvFile = file => {
        const reader = new FileReader();

        reader.onloadend = () => {
            const request = new Request("/account/uploadcsv", {
                "method": "POST",
                "headers": {
                    "Content-Type": "application/json"
                },
                "body": JSON.stringify({
                    "csvString": reader.result
                })
            });

            fetch(request)
                .then(response => response.json())
                .then(data => {
                    if (!data.success) {
                        setErrorResponse(data);

                        return;
                    }

                    alert(`Failed: ${data.response.failCount}\r\nSucceeded: ${data.response.successCount}`);
                });
        }

        reader.readAsText(file);
    };

    return (
        <React.Fragment>
            <h1>Accounts</h1>

            <section className="accounts-areas">
                <div
                    className={csvDropClassName}
                    onDragLeave={event => dragLeaveHandler(event)}
                    onDragOver={event => dragOverHandler(event)}
                    onDrop={event => dropHandler(event)}
                >
                    <h2>CSV Upload</h2>

                    <p>Drag and drop CSV</p>

                    {
                        errorResponse != null &&
                        <p>{errorResponse}</p>
                    }

                    <input
                        type="file"
                        ref={fileInputRef}
                        style={{"display": "none"}} />
                </div>

                <div className="accounts-areas__links">
                    <button>Add reading</button>
                </div>
            </section>
        </React.Fragment>
    )
};

export default Accounts;