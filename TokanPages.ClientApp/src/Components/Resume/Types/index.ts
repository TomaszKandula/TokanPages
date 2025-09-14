import { TagType } from "../../../Shared/Components/RenderHtml/types";
import { ResumeContentDto, TestimonialsContentDto } from "../../../Api/Models";
import { ViewProperties } from "../../../Shared/Abstractions";

export interface ResumeViewProps extends ViewProperties {
    className?: string;
    page: ResumeContentDto;
    section: TestimonialsContentDto;
}

export interface RenderCaptionProps {
    isLoading: boolean;
    text: string;
    tag?: TagType;
}
