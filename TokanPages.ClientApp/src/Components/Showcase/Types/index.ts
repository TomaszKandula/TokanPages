import { FeatureShowcaseContentDto } from "../../../Api/Models";

export interface ShowcaseViewProps {
    className?: string;
}

export interface ActiveButtonProps extends FeatureShowcaseContentDto {
    isLoading: boolean;
}
