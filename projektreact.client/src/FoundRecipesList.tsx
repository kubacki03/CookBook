import React, { useState } from "react";

type Ingredient = {
    id: number;
    recipeId: number;
    ingredientName: string;
    weight: number;
};

type Recipe = {
    id: number;
    userId?: string | null;
    title: string;
    description?: string | null;
    ingredients: Ingredient[];
};

type FoundRecipesListProps = {
    recipes: Recipe[];
};

const FoundRecipesList: React.FC<FoundRecipesListProps> = ({ recipes }) => {
    const [hoveredRecipeId, setHoveredRecipeId] = useState<number | null>(null);

    if (recipes.length === 0) {
        return <p>Brak wyników.</p>;
    }

    return (
        <ul style={{ marginTop: 20 }}>
            {recipes.map((recipe) => (
                <li
                    key={recipe.id}
                    style={{ marginBottom: 20, cursor: "pointer", position: "relative" }}
                    onMouseEnter={() => setHoveredRecipeId(recipe.id)}
                    onMouseLeave={() => setHoveredRecipeId(null)}
                >
                    <strong>{recipe.title}</strong>
                    {recipe.description && <p>{recipe.description}</p>}

                    {hoveredRecipeId === recipe.id && recipe.ingredients.length > 0 && (
                        <ul style={{
                            marginTop: 10,
                            paddingLeft: 20,
                            backgroundColor: "#f9f9f9",
                            border: "1px solid #ccc",
                            borderRadius: 8,
                            padding: 10,
                            position: "absolute",
                            zIndex: 1
                        }}>
                            {recipe.ingredients.map((ingredient) => (
                                <li key={ingredient.id}>
                                    {ingredient.ingredientName} – {ingredient.weight}g
                                </li>
                            ))}
                        </ul>
                    )}
                </li>
            ))}
        </ul>
    );
};

export default FoundRecipesList;
