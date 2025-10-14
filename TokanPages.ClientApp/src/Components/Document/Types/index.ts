import { TextItem } from "../../../Shared/Components/RenderContent/Models";

export interface DocumentViewProps {
    isLoading: boolean;
    items: TextItem[];
    className?: string;
}
