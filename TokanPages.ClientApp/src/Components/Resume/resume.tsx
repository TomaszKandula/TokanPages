import React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ProcessedExperienceItemProps } from "./Types";
import { GetTimeRange } from "./Utilities";
import { ResumeView } from "./View/resumeView";

export const Resume = (): React.ReactElement => {
    const page = useSelector((state: ApplicationState) => state.contentPageData.components.pageResume);
    const testimonials = useSelector((state: ApplicationState) => state.contentPageData.components.sectionTestimonials);
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const languageId = useSelector((state: ApplicationState) => state.applicationLanguage.id);
    const isContentLoading = data.isLoading;

    const [experienceItems, setExperienceItems] = React.useState<ProcessedExperienceItemProps[] | undefined>(undefined);

    React.useEffect(() => {
        if (page.resume.experience.list.length > 0) {
            const processedItems = GetTimeRange(page.resume.experience.list);
            setExperienceItems(processedItems);
        }
    }, [page.resume.experience.list]);

    return (
        <ResumeView
            isLoading={isContentLoading}
            languageId={languageId}
            page={page}
            section={testimonials}
            processed={experienceItems ?? []}
        />
    );
};
