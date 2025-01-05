using System;
using System.Collections.Generic;

class Program
{
	static void Main(string[] args)
	{
		List<Item> items = new List<Item>
		{
			new Item(0),
			new Item(1),
			new Item(2),
		};
		Player player = new Player(items);
		List<Item> playerItems = (List<Item>)player.Items;
		playerItems.Add(new Item(-1));
		foreach (Item item in player.Items)
			Console.WriteLine(item.Id);
	}
}

class Player
{
	private List<Item> _items = new List<Item>();
	public IReadOnlyList<Item> Items => _items;
	public Player(IEnumerable<Item> items) =>
		_items.AddRange(items);
}
class Item
{
	public Item(int id) =>
		Id = id;
	public int Id { get; }
}