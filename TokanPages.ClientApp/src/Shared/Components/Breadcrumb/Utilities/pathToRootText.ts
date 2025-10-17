import { ItemDto } from "../../../../Api/Models";
import { PathProps, PathToRootTextResultProps } from "../Types";

export const pathToRootText = (props: PathProps): PathToRootTextResultProps => {
    const array = props.pathname.split("/");
    const fragments = array.filter(e => String(e).trim());
    const rootWithHash = `#${fragments[0]}`;
    const rootWithSlash = `/${fragments[0]}`;
    let hasHash: boolean = false;

    const text = props.navigation.menu.items.find((item: ItemDto) => {
        const hasNavbar = item.navbarMenu?.enabled === true;
        const link = item.link?.toUpperCase();
        const hashRoot = rootWithHash.toUpperCase();
        const slashRoot = rootWithSlash.toUpperCase();

        if (hasNavbar && link?.includes(hashRoot)) {
            hasHash = true;
            return item;
        }

        if (hasNavbar && link?.includes(slashRoot)) {
            return item;
        }

        return undefined;
    });

    return {
        value: text?.value ?? "",
        hasHash: hasHash,
    };
};
