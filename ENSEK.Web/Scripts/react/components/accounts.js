import React, { useEffect, useRef, useState } from "react";

import MeterReadings from "./meterReadings";

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
    const [meterReadings, setMeterReadings] = useState(null);
    const [processing, setProcessing] = useState(false);
    const [successResponse, setSuccessResponse] = useState(null);

    const dragLeaveHandler = event => {
        setCsvDropClassName(defaultCsvDropClassName);
    };

    const dragOverHandler = event => {
        setCsvDropClassName(`${defaultCsvDropClassName} dragover`);
    };

    const dropHandler = event => {
        setErrorResponse(null);
        setSuccessResponse(null);
        setProcessing(true);

        setCsvDropClassName(defaultCsvDropClassName);

        if (event.dataTransfer.files == null || event.dataTransfer.files.length === 0) {
            alert("Please drag and drop a CSV file into the drop area.");
            setProcessing(false);

            return;
        }

        const firstFile = event.dataTransfer.files[0];

        if (!firstFile.name.toLowerCase().endsWith(".csv")) {
            alert("Please upload a CSV file.");
            setProcessing(false);

            return;
        }

        uploadCsvFile(firstFile);
    };

    const getMeterReadings = () => {
        fetch("/account/meterreadings")
            .then(response => response.json())
            .then(data => {
                if (data.meterReadings == null) {
                    return;
                }

                setMeterReadings(data.meterReadings);
            });
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
                    if (data.response == null) {
                        setErrorResponse("Error uploading CSV");

                        return;
                    }

                    getMeterReadings();

                    setSuccessResponse(data.response);
                })
                .finally(() => {
                    setProcessing(false);
                });
        }

        reader.readAsText(file);
    };

    useEffect(() => {
        getMeterReadings();
    }, []);

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
                        processing &&
                        <div className="processing-spinner">
                            <i className="fas fa-spinner fa-pulse"></i>
                        </div>
                    }

                    {
                        errorResponse != null &&
                        <p className="error">{errorResponse}</p>
                    }

                    {
                        successResponse != null &&
                        <div>
                            <p className="success">Successful entries: {successResponse.successCount}</p>
                            <p className="error">Failed entries: {successResponse.failCount}</p>
                        </div>
                    }

                    <input
                        type="file"
                        ref={fileInputRef}
                        style={{"display": "none"}} />
                </div>

                <div className="accounts-areas__extras">
                    <MeterReadings meterReadings={meterReadings} />
                </div>
            </section>
        </React.Fragment>
    )
};

export default Accounts;