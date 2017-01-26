using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TodoModels.Models
{
    public class TodoNode
    {
        [Key]
        public int TodoNodeId { get; set; }

        [Required]
        public string Title { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreationTime { get; set; }

        [ScaffoldColumn(false)]
        public DateTime LastModifiedTime { get; set; }

        public string Description { get; set; }

        [ScaffoldColumn(false)]
        public bool IsRoot { get; set; }

        public virtual ICollection<TodoNodeParentMap> Children { get; set; }
    }

    public class TodoNodeParentMap
    {
        [Key]
        public int TodoNodeParentMapId{ get; set; }
        public int TodoNodeId { get; set; }
        public int ParentNodeId { get; set; }

        [ForeignKey(nameof(TodoNodeId))]
        [InverseProperty(nameof(TodoModels.Models.TodoNode.Children))]
        public virtual TodoNode TodoNode { get; set; }

        [ForeignKey(nameof(ParentNodeId))]
        public virtual TodoNode ParentNode { get; set; }
    }
}
