import type { ReactNode } from 'react'

type DashboardGridProps = {
  children: ReactNode
}

function DashboardGrid({ children }: DashboardGridProps) {
  return <section className="dashboard-grid">{children}</section>
}

export default DashboardGrid
