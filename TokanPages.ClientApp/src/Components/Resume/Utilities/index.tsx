import React from "react";
import { ExperienceItemProps } from "../../../Api/Models";
import { ProcessedExperienceItemProps, ProcessTimeSpanProps } from "../Types";
import Validate from "validate.js";

const monthDiff = (dateFrom: Date, dateTo: Date) => {
    const offset = 1;
    const dateToFullYear = dateTo.getFullYear();
    const dateFromFullYear = dateFrom.getFullYear();
    const dateToMonth = dateTo.getMonth();
    const dateFromMonth = dateFrom.getMonth();

    // Set offset value to '1' because we count from the beggining of the month to the end of the month.
    return dateToMonth - dateFromMonth + offset + 12 * (dateToFullYear - dateFromFullYear);
};

const tryParseYear = (date: string): number => {
    //Format: mm.yyyy
    const result = Number(date.substring(date.length - 4, date.length));

    if (Validate.isNumber(result)) {
        return result;
    }

    return new Date().getFullYear();
};

const tryParseMonth = (date: string): number => {
    //Format: mm.yyyy
    const result = Number(date.substring(0, 2));

    if (Validate.isNumber(result)) {
        return result;
    }

    return new Date().getMonth();
};

export const GetTimeRange = (items: ExperienceItemProps[]): ProcessedExperienceItemProps[] => {
    const result: ProcessedExperienceItemProps[] = [];

    items.forEach(item => {
        let timespan = 0;
        item.occupation.forEach(occupation => {
            const getYearStart = tryParseYear(occupation.dateStart);
            const getMonthStart = tryParseMonth(occupation.dateStart);
            const getYearEnd = tryParseYear(occupation.dateEnd);
            const getMonthEnd = tryParseMonth(occupation.dateEnd);

            const dateStart = new Date(getYearStart, getMonthStart);
            const dateEnd = new Date(getYearEnd, getMonthEnd);

            const diff = monthDiff(dateStart, dateEnd);
            timespan += diff;
        });

        result.push({ ...item, timespan: timespan });
    });

    return result;
};

export const ProcessTimeSpan = (props: ProcessTimeSpanProps): React.ReactElement => {
    if (!props.months) {
        return <></>;
    }

    if (props.months > 12) {
        const years = Math.floor(props.months / 12);
        const month = props.months % 12;

        if (month === 0) {
            return (
                <>
                    {years} {years > 1 ? props.yearsLabel : props.yearLabel}
                </>
            );
        }

        return (
            <>
                {years} {years > 1 ? props.yearsLabel : props.yearLabel} {month}{" "}
                {month > 1 ? props.monthsLabel : props.monthLabel}
            </>
        );
    }

    return <>{props.months > 1 ? `${props.months} ${props.monthsLabel}` : `${props.months} ${props.monthLabel}`}</>;
};
