export interface TextObject {
    items: TextItem[];
}

export interface ConstraintProps {
    maxWidth?: number;
    maxHeight?: number;
}

export interface TextItem {
    id: string;
    type: string;
    value: string | RowItem[];
    prop: string;
    text: string;
    textId?: string;
    propTitle?: string;
    propSubtitle?: string;
    propImg?: string;
    constraint?: ConstraintProps;
}

export interface RowItem {
    column0: string;
    column1: string;
    column2: string;
}
