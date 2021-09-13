import { IFields } from "./fields";
import { ISubitem } from "./subitem";

export interface IItem extends IFields
{
    subitems?: ISubitem[];
}
