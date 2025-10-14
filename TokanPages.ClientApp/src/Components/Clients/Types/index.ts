import { ClientsContentDto } from "../../../Api/Models";

export interface ClientsViewProps {
    className?: string;
}

export interface ClientsViewExtendedProps extends ClientsContentDto {
    isLoading: boolean;
}
