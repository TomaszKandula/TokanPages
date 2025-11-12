import { TLoading, TObjectFit } from "../../../../../Shared/Types";

export interface TextObject {
    items: TextItem[];
}

export interface ConstraintProps {
    width?: number;
    height?: number;
    objectFit?: TObjectFit;
}

export interface TextItem {
    id: string;
    type: string;
    value: string | string[] | RowItem[] | ImageItemProps[] | VideoItemProps[];
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

export interface ImageItemProps {
    image: string;
    caption: string;
    constraint?: ConstraintProps;
}

export interface VideoItemProps {
    poster: string;
    video: string;
    text: string;
    constraint?: ConstraintProps;
}
