import { ItemDto } from "Api/Models";
import { NavigationProps } from "../Types";

export const getHomeText = (navigation: NavigationProps): string => {
    const text = navigation.menu.items.find((item: ItemDto) => {
        if (item.link === `/${navigation.language}`) {
            return item;
        }

        return undefined;
    });

    return text?.value ?? "";
};
