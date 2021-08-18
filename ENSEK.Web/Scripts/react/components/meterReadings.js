import React, { useEffect, useState } from "react";
import * as dateFormat from "dateformat";

import "./meterReadings.scss";

const MeterReadings = props => {
    let readingKey = 0;

    return (
        <React.Fragment>
            <h2>Meter Readings</h2>

            <table className="meter-readings-table">
                <thead>
                    <tr>
                        <th>Account ID</th>
                        <th>Date and Time</th>
                        <th>Value</th>
                    </tr>
                </thead>

                <tbody>
                    {
                        props.meterReadings &&
                        props.meterReadings.map(reading =>
                            <tr key={readingKey++}>
                                <td>{reading.accountId}</td>
                                <td>{dateFormat(reading.dateTime, "dd/mm/yyyy HH:MM")}</td>
                                <td>{reading.value.toString().padStart(5, "0")}</td>
                            </tr>
                        )
                    }
                </tbody>
            </table>
        </React.Fragment>
    )
};

export default MeterReadings;