import React, { useEffect, useState } from "react";

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

    const [csvDropClassName, setCsvDropClassName] = useState(defaultCsvDropClassName);

    const dragLeaveHandler = event => {
        setCsvDropClassName(defaultCsvDropClassName);
    };

    const dragOverHandler = event => {
        setCsvDropClassName(`${defaultCsvDropClassName} dragover`);
    };

    const dropHandler = event => {
        setCsvDropClassName(defaultCsvDropClassName);
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
                </div>

                <div className="accounts-areas__links">
                    <button>Add reading</button>
                </div>
            </section>
        </React.Fragment>
    )
};

export default Accounts;