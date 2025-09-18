import React from "react";
import { ProcessTimeSpanProps } from "../Types";

export const ProcessTimeSpan = (props: ProcessTimeSpanProps): React.ReactElement => {
    if (!props.months) {
        return <></>;
    }

    if (props.months > 12) {
        const years = Math.floor(props.months / 12);
        const month = props.months % 12;

        return (
            <>
                {years} {years > 1 ? props.yearsLabel : props.yearLabel} {month}{" "}
                {month > 1 ? props.monthsLabel : props.monthLabel}
            </>
        );
    }

    return <>{props.months > 1 ? `${props.months} ${props.monthsLabel}` : `${props.months} ${props.monthLabel}`}</>;
};

