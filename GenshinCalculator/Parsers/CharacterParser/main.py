import requests
import threading
import re
import csv
from bs4 import BeautifulSoup
from datetime import datetime


def parse(url):
    r = requests.get(url)
    if r.status_code == 200:
        return BeautifulSoup(r.content, features="lxml")
    else:
        with lock:
            print("Code:", r.status_code)
        return False


# region Global Vars.

# region ---- Custom

main_page_url = "https://genshin-impact.fandom.com"
start_page_url = "https://genshin-impact.fandom.com/ru/wiki/Персонажи"

# Export

system_name = "CharacterCSV"

# Table

table_column_position = {
    "icon": 1,
    "name": 2,
    "rarity": 3,
    "vision": 4,
    "weapon_type": 5,
    "region": 6
}

# endregion

lock = threading.Lock()
thread_count = 16

character_dict = dict()
character_url_set = set()

# endregion

# region Add. Functions


def get_characters_table():
    page = parse(start_page_url)
    span_tag_element = page.select_one("#Играбельные_персонажи")
    table = span_tag_element.find_next("table")
    return table


def get_character_urls(table):
    content = table.select("tbody > tr")
    for character_row in content[1:]:
        url = main_page_url + character_row.select_one(f"td:nth-child({table_column_position['name']}) > a").get("href")
        character_url_set.add(url)


# region ---- Table Info


def get_table_info(table):
    content = table.select("tbody > tr")
    for character_row in content[1:]:
        character = {
            "name": get_name(character_row),
            "rarity": get_rarity(character_row),
            "region": get_region(character_row)
        }
        character_dict[character["name"]] = character
    return True


def get_name(character_row):
    return character_row.select_one(f"td:nth-child({table_column_position['name']}) > a").getText()


def get_rarity(character_row):
    img = character_row.select_one(f"td:nth-child({table_column_position['rarity']}) > p > img")
    return re.search(r'(\d+)', img.get("alt")).group(1)


def get_region(character_row):
    a_arr = character_row.select(f"td:nth-child({table_column_position['region']}) > a")
    region_arr = list(map(lambda x: x.get("title"), a_arr))
    return ", ".join(region_arr)


# endregion

# endregion

# region Threading Func.


def launch_inspect_element():
    thread_set = set()

    for i in range(thread_count):
        thread = threading.Thread(target=inspect_element)
        thread_set.add(thread)
        thread.start()

    for thread in thread_set:
        thread.join()


def inspect_element():
    while True:
        with lock:
            if character_url_set:
                url = character_url_set.pop()
            else:
                break
        try:
            page = parse(url)

            if page:
                en_page = get_en_page(page)
                name = page.select_one("h2[data-source='Имя']").getText()
                element = get_page_info(page, en_page)
                with lock:
                    for key in element.keys():
                        character_dict[name][key] = element[key]
            else:
                with lock:
                    print("func. parse: Error. URL:", url)
                    character_url_set.add(url)
        except requests.ConnectionError:
            with lock:
                print("func. inspect_element: Connection Error. URL:", url)
                character_url_set.add(url)


def get_page_info(page, en_page):

    data = {
        "fullname": get_fullname(page),
        "avatar_path": get_avatar_path(en_page),
        "description": get_description(page),
        "day_of_birth": get_day_of_birth(en_page),
        "vision": get_vision(en_page),
        "weapon_type": get_weapon_type(en_page)
    }
    return data


def get_en_page(page):
    return parse(page.select_one("div.page-header__languages a[data-tracking-label='lang-en']").get("href"))


def get_fullname(page):
    div = page.select_one("div[data-source='Полное_имя'] > div")
    if div:
        return div.getText()
    return None


def get_avatar_path(en_page):
    url = main_page_url + en_page.select_one("table.custom-tabs").find(text="Media").parent.get("href")
    media_page = parse(url)
    span_tag_element = media_page.select_one("#In_Game_Assets")
    img = span_tag_element.find_next("img")
    img_src = re.search(r'(.+\.png)', img.get("src")).group(1)
    return img_src


def get_description(page):
    return page.select_one("div[data-item-name='description'] > div").getText()


def get_day_of_birth(en_page):
    a = en_page.select_one("div[data-source='birthday'] > div > a")
    match = re.search(r'(\w+) (\d+)', a.getText())
    if match:
        date = datetime.strptime(match.group(1), "%B")
        return f"2000.{date.strftime('%m')}.{match.group(2)}"
    return None


def get_vision(en_page):
    a = en_page.select_one("td[data-source='element'] > span > a")
    if a:
        return a.get("title")
    return None


def get_weapon_type(en_page):
    return en_page.select_one("td[data-source='weapon'] > span > a").get("title")


# endregion

# region Main


def main():
    pass


def test():
    table = get_characters_table()
    get_table_info(table)
    get_character_urls(table)
    launch_inspect_element()
    print(character_dict)


# endregion

if __name__ == '__main__':
    test()
