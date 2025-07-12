import { ItemDto, SubitemDto } from "../../../../Api/Models";
import { PathProps } from "../Types";

export const pathToSubitemText = (props: PathProps): string => {
    const array = props.pathname.split("/");
    const fragments = array.filter(e => String(e).trim());
    const root = `#${fragments[0]}`;

    const itemWithSubitem = props.navigation.menu.items.find((item: ItemDto) => {
        const hasNavbar = item.navbarMenu?.enabled === true;
        const link = item.link?.toUpperCase();
        const rootValue = root.toUpperCase();

        if (hasNavbar && link?.includes(rootValue) && item.subitems !== undefined) {
            return item;
        }

        return undefined;
    });

    if (itemWithSubitem?.subitems) {
        const text = itemWithSubitem?.subitems.find((subitem: SubitemDto) => {
            const link = subitem.link?.toUpperCase();
            const pathname = props.pathname.toUpperCase();

            if (link?.includes(pathname)) {
                return subitem;
            }

            return undefined;
        });

        return text?.value ?? "";
    }

    return "";
};
