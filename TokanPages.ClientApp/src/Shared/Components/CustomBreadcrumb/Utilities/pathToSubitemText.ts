import { ItemDto, SubitemDto } from "../../../../Api/Models";
import { PathProps } from "../Types";

export const pathToSubitemText = (props: PathProps): string => {
    const array = props.pathname.split("/");
    const fragments = array.filter(e => String(e).trim());
    const root = `#${fragments[0]}`;

    const itemWithSubitem = props.navigation.menu.items.find((item: ItemDto) => {
        if (item.link?.toUpperCase().includes(root.toUpperCase()) && item.subitems !== undefined) {
            return item;
        }

        return undefined;
    });

    if (itemWithSubitem?.subitems) {
        const text = itemWithSubitem?.subitems.find((subitem: SubitemDto) => {
            if (subitem.link?.toUpperCase().includes(props.pathname.toUpperCase())) {
                return subitem;
            }

            return undefined;
        });

        return text?.value ?? "";
    }

    return "";
};
