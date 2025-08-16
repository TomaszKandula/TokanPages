import { TLoading } from "../../../../../Shared/types";

export interface TextObject {
    items: TextItem[];
}

export interface ConstraintProps {
    width?: number;
    height?: number;
}

export interface TextItem {
    id: string;
    type: string;
    value: string | string[] | RowItem[];
    prop: string;
    text: string;
    propTitle?: string;
    propSubtitle?: string;
    propImg?: string;
    loading?: TLoading;
    constraint?: ConstraintProps;
}

export interface RowItem {
    column0: string;
    column1: string;
    column2: string;
}
