export type TList = "ul" | "ol";

export interface RenderListProps {
    list: string[];
    type?: TList;
    className?: string;
    dataTestId?: string;
    isLoading?: boolean;
}
