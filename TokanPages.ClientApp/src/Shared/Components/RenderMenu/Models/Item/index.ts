import { Fields } from "../Fields";
import { Subitem } from "../Subitem";

export interface Item extends Fields {
    subitems?: Subitem[];
}
