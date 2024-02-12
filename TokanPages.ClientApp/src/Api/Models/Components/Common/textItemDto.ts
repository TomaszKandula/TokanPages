import { RowItemDto } from "./rowItemDto";

export interface TextItemDto {
    id: string;
    type: string;
    value: string | RowItemDto[];
    prop: string;
    text: string;
}
