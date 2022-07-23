import { IFields } from "../Fields";
import { ISubitem } from "../Subitem";

export interface IItem extends IFields
{
    subitems?: ISubitem[];
}
