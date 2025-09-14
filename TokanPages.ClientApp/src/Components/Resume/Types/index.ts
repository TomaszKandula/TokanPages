import { ResumeContentDto } from "../../../Api/Models";
import { ViewProperties } from "../../../Shared/Abstractions";

export interface ResumeViewProps extends ViewProperties {
    className?: string;
    content: ResumeContentDto;
}

export interface RenderCaptionProps {
    isLoading: boolean;
    text: string;
}
