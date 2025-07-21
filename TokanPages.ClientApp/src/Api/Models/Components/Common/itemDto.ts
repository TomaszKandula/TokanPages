import { FieldsDto } from "./fieldsDto";
import { SubitemDto } from "./subItemDto";

export interface ItemDto extends FieldsDto {
    subitems?: SubitemDto[];
}
