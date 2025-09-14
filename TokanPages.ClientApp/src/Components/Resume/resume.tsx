import React from "react";
import { useSelector } from "react-redux";
import { ExperienceItemProps } from "../../Api/Models";
import { ApplicationState } from "../../Store/Configuration";
import { ProcessedExperienceItemProps } from "./Types";
import { ResumeView } from "./View/resumeView";
import Validate from "validate.js";

const monthDiff = (dateFrom: Date, dateTo: Date) => {
    const offset = 1;
    const dateToFullYear = dateTo.getFullYear();
    const dateFromFullYear = dateFrom.getFullYear();
    const dateToMonth = dateTo.getMonth();
    const dateFromMonth = dateFrom.getMonth();

    // Set offset value to '1' because we count from the beggining of the month to the end of the month.
    return (dateToMonth - dateFromMonth + offset) + (12 * (dateToFullYear - dateFromFullYear));
}

const tryParseYear = (date: string): number => {
    //Format: mm.yyyy
    const result =  Number(date.substring(date.length - 4, date.length));

    if (Validate.isNumber(result)) {
        return result;
    }

    return new Date().getFullYear();
}

const tryParseMonth = (date: string): number => {
    //Format: mm.yyyy
    const result =  Number(date.substring(0, 2));

    if (Validate.isNumber(result)) {
        return result;
    }

    return new Date().getMonth();
}

const processTimespan = (items: ExperienceItemProps[]): ProcessedExperienceItemProps[] => {
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
}

export const Resume = (): React.ReactElement => {
    const page = useSelector((state: ApplicationState) => state.contentPageData.components.pageResume);
    const testimonials = useSelector((state: ApplicationState) => state.contentPageData.components.sectionTestimonials);
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const isContentLoading = data.isLoading;

    const [experienceItems, setExperienceItems] = React.useState<ProcessedExperienceItemProps[] | undefined>(undefined);

    React.useEffect(() => {
        if (page.resume.experience.list.length > 0 && !experienceItems) {
            const processedItems = processTimespan(page.resume.experience.list);
            setExperienceItems(processedItems);
        }
    }, [page.resume.experience.list]);

    return <ResumeView isLoading={isContentLoading} page={page} section={testimonials} processed={experienceItems ?? []} />;
};
