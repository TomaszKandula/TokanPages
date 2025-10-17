export interface ProgressStyleBaseProperties {
    height: number;
    bgcolor: string;
    duration: number;
}

export interface ProgressStyleProps extends ProgressStyleBaseProperties {
    width: number;
}

export interface ClearPageStartProps {
    children: React.ReactElement | React.ReactElement[];
}
