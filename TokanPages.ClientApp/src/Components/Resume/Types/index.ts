import { TagType } from "../../../Shared/Components/RenderHtml/types";
import { ExperienceItemProps, ResumeContentDto, TestimonialsContentDto } from "../../../Api/Models";
import { ViewProperties } from "../../../Shared/Types";

export interface ProcessedExperienceItemProps extends ExperienceItemProps {
    timespan: number;
}

export interface ResumeViewProps extends ViewProperties {
    className?: string;
    languageId: string;
    page: ResumeContentDto;
    section: TestimonialsContentDto;
    processed: ProcessedExperienceItemProps[];
}

export interface RenderCaptionProps {
    isLoading: boolean;
    text: string;
    tag?: TagType;
}

export interface ProcessTimeSpanProps {
    months: number;
    yearLabel: string;
    monthLabel: string;
    yearsLabel: string;
    monthsLabel: string;
}
