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
}
